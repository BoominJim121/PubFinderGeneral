import {
  URLSearchParamsInit
} from './types';
//@ts-ignore
import ServiceBase from './serviceBase.ts';

class AuthService extends ServiceBase {
  service: string;
  accessToken: string | null;

  constructor(service: string) {
    super();
    this.accessToken = null;
  }

  protected async fetch<TResult>(
    init: RequestInit & {
      path: string;
      params?: URLSearchParamsInit;
    }
  ): Promise<TResult> {
    //this.accessToken = await this.getToken(); here is where i would go and get an authtoken if one was required. 
    //if (this.accessToken) {
      return super.fetch(init);
    //}

    throw new Error('no access token');
  }
} 

export default AuthService;