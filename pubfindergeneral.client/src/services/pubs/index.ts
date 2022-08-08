import { IPubFinderGeneralDataParams, IPublicHouseResponse } from "../../Types/types";
import AuthService from "../base/AuthService";

class PubsService extends AuthService
{
    constructor(baseURL: string){
        super('pubs-data-api')
        this.initialize({ baseURL });
    }

    async GetPubs( params: IPubFinderGeneralDataParams
      ): Promise<IPublicHouseResponse> {
        return await this.get(
          `/pubs?pageNumber=${params.pageNumber}&pageSize=${params.pageSize}`, 
        );
      }
}
export default PubsService;