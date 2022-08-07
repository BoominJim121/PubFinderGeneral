const REDACTED_TOKEN = '[REDACTED]';

const redactAuthHeader = (headerValue: string) =>
  headerValue.replace(/([^\s]+\s+)?(\S+)/g, `${REDACTED_TOKEN}`);

/** Strips sensitive information from errors before logging to console or error tracking services */
// eslint-disable-next-line @typescript-eslint/no-explicit-any
export const errorRedactionInterceptor = (error: any) => {
  if (error.config?.data) {
    error.config.data = REDACTED_TOKEN;
  }

  if (error.config?.body) {
    error.config.body = REDACTED_TOKEN;
  }

  if (error.config?.headers?.Authorization) {
    error.config.headers.Authorization = redactAuthHeader(
      error.config.headers.Authorization
    );
  }

  if (error.response?.config?.headers?.Authorization) {
    error.response.config.headers.Authorization = redactAuthHeader(
      error.config.headers.Authorization
    );
  }

  if (error.request?._header) {
    error.request._header = REDACTED_TOKEN;
  }

  if (error.response?.request?._header) {
    error.response.request._header = REDACTED_TOKEN;
  }

  if (error.response?.config?.data) {
    error.response.config.data = REDACTED_TOKEN;
  }

  if (error.response?.config?.body) {
    error.response.config.body = REDACTED_TOKEN;
  }

  throw error;
};
