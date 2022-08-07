import { IPublicHouse } from "../../Types/types";
//@ts-ignore
import AuthService from "../base/AuthService.ts";

class PubsService extends AuthService
{
    constructor(baseURL: string){
        super('pubs-data-api');
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