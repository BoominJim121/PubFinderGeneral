import axios, { AxiosInstance, AxiosRequestConfig } from 'axios';
import {
  RequestOptions,
  URLSearchParamsInit,
  Base,
} from './types';
//@ts-ignore
import { errorRedactionInterceptor } from '../interceptors/errorRedactionInterceptor.ts';

export function createAxiosInstance(baseUrl: string): AxiosInstance {
    const axiosInstance = axios.create({
      baseURL: baseUrl
    });
    
    const ENABLE_AXIOS_TIMING = process.env.ENABLE_AXIOS_TIMING === 'true';
    const AXIOS_ERROR_REPORTING_ENABLED =
    process.env.AXIOS_ERROR_REPORTING_ENABLED === 'true';
    
    axiosInstance.interceptors.request.use(config => {
      if (ENABLE_AXIOS_TIMING) {
        // to avoid overwriting if another interceptor
        // already defined the same object (meta)
        //@ts-ignore
        config.meta = config.meta || {};
        //@ts-ignore
        config.meta.requestStartedAt = new Date().getTime();
      }
      if (process.env.NODE_ENV === 'development') {
        // eslint-disable-next-line no-console
        console.log(
          config.method?.toUpperCase(),
          baseUrl,
          config.url,
          config.params ? config.params.toString() : config.data?.body?.toString()
        );
      }
      return config;
    });
  
    if (ENABLE_AXIOS_TIMING) {
      axiosInstance.interceptors.response.use(
        x => {
          // eslint-disable-next-line no-console
          console.log(
            `Execution time for: ${baseUrl}/${x.config.url} - ${
              //@ts-ignore
              new Date().getTime() - x.config.meta.requestStartedAt
            } ms`
          );
          return x;
        },
        // Handle 4xx & 5xx responses
        x => {
          console.error(
            `Execution time for: ${x.config.url} - ${
              new Date().getTime() - x.config.meta.requestStartedAt
            } ms`
          );
          throw x;
        }
      );
    }
  
    axiosInstance.interceptors.response.use(undefined, errorRedactionInterceptor);
  
    return axiosInstance;
  }

abstract class ServiceBase implements Base {
    baseUrl!: string;
    protected willSendRequest?(request: RequestOptions): void | Promise<void>;
    private axios!: AxiosInstance & {
      [key: string]: any;
    };
  
    initialize(config: { baseURL: string }): void {
      this.baseUrl = config.baseURL;
      this.axios = createAxiosInstance(config.baseURL);
    }
  
    public async get<TResult>(
      path: string,
      params?: URLSearchParamsInit,
      init?: RequestInit & Partial<AxiosRequestConfig>
    ): Promise<TResult> {
      return this.fetch<TResult>(
        Object.assign({ method: 'get', path, params }, init)
      );
    }

    protected async fetch<TResult>(
        init: RequestInit & {
        path: string;
        params?: URLSearchParamsInit;
        }
    ): Promise<TResult> {
        if (!init.method) {
        throw new Error('A method must be passed');
        }

        if (!(init.params instanceof URLSearchParams)) {
        init.params = new URLSearchParams(init.params);
        }

        if (!init.headers) {
        init.headers = {};
        }

        const options = init as RequestOptions;

        if (this.willSendRequest) {
        await this.willSendRequest(options);
        }

        // We accept arbitrary objects and arrays as body and serialize them as JSON
        if (
        options.body !== undefined &&
        options.body !== null &&
        (options.body.constructor === Object || Array.isArray(options.body))
        ) {
        options.body = JSON.stringify(options.body);

        // If Content-Type header has not been previously set, set to application/json
        if (!options.headers['Content-Type']) {
            options.headers['Content-Type'] = 'application/json; charset=utf-8';
        }
        }

        const config: AxiosRequestConfig = {
        url: init.path,
        data: options.body,
        ...options
        };

        const res = await this.axios(config);
        return res.data;
    }

    public getAxiosInstance(): AxiosInstance {
        return this.axios;
    }
}

export default ServiceBase;