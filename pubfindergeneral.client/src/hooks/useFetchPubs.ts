//@ts-ignore
import services from '../services/services.ts';
import { IPublicHouse } from '../Types/types';
import { AxiosError } from 'axios';
import { useQuery } from 'react-query';

const useFetchPubs = () =>
  useQuery<IPublicHouse[], AxiosError>(
    ['public-house-request'],
    () =>
      services.pubsService.GetPubs(),
    {
      retry: false,
      refetchOnMount: false
    }
  );

  export default useFetchPubs;