//@ts-ignore
import services from '../services/services.ts';
import { IPubFinderGeneralDataParams, IPublicHouseResponse } from '../Types/types';
import { AxiosError } from 'axios';
import { useQuery } from 'react-query';

const useFetchPubs = (params: IPubFinderGeneralDataParams) =>
  useQuery<IPublicHouseResponse, AxiosError>(
    ['public-house-request'],
    () =>
      services.pubsService.GetPubs(params),
    {
      retry: false,
      refetchOnMount: false
    }
  );

  export default useFetchPubs;