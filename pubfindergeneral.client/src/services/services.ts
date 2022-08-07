//@ts-ignore
import PubsService from './pubs/index.ts';

export default { 
    pubsService: new PubsService(
    `http://localhost:3001`
  )
}