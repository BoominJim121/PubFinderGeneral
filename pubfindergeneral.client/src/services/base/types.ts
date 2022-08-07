/* eslint-disable @typescript-eslint/no-explicit-any */

import { Method } from 'axios';

export type URLSearchParamsInit =
  | URLSearchParams
  | string
  | Record<string, any>;

export type BodyInitOrObject =
  | BodyInit
  | {
      [key: string]: any;
    };

export interface Base {
    initialize?(config: { baseURL: string }): void | Promise<void>;
  }
  export type RequestOptions = RequestInit & {
    path: string;
    params: URLSearchParams;
    headers: any;
    body?: Body | string;
    method: Method;
    signal?: AbortSignal;
  };