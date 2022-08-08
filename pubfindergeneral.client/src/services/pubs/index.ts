import { IPublicHouse } from "../../Types/types";
import AuthService from "../base/AuthService";

class PubsService extends AuthService
{
    constructor(baseURL: string){
        super('pubs-data-api')
        this.initialize({ baseURL });
    }

    async GetPubs(
      ): Promise<IPublicHouse[]> {
        return await this.get(
          `/pubs`
        );
      }
}
export default PubsService;