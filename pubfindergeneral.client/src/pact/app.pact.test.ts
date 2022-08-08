import { Matchers, Pact } from '@pact-foundation/pact';
import  PubsService  from '../services/pubs/index'
const path = require('path');


const pactProvider = new Pact({
    consumer: 'pub-finder-general-client',
    provider: 'pub-finder-general-data-api',
    log: path.resolve(__dirname, '../../pact-logs', 'pact.log'),
    dir: path.resolve(__dirname, '../../pacts')
  });

  let pubService: PubsService;

  beforeAll(async () => {
    await pactProvider.setup();
    pubService = new PubsService(
      `http://localhost:${pactProvider.opts.port}`
    );
  });

  afterEach(async () => {
    await pactProvider.verify();
  });

  afterAll(async () => {
    await pactProvider.finalize();
  });

  describe('Pubs', () => {
    describe('Get Pubs', () => {
      it('returns 200 with expected response', async () => {
        await pactProvider.addInteraction({
          state: 'Pub Results Exist',
          uponReceiving: 'A request to get Pubs',
          withRequest: {
            method: 'GET',
            path: `/pubs`,
            headers: {
              Accept: 'application/json, text/plain, */*'
            }
          },
          willRespondWith: {
            status: 200,
            headers: {
              'Content-Type': 'application/json; charset=utf-8'
            },
            body: {
              PubJsonList: Matchers.eachLike({
                name: Matchers.string('...escobar'),
                category: Matchers.string('Closed Venues'),
                lastUpdatedDateTime: Matchers.string('2012-11-30T21:58:52+00:00'),
                about: Matchers.like({
                  website:Matchers.string("http://leedsbeer.info/?p=765"),
                  thumbnail:Matchers.string("http://leedsbeer.info/wp-content/uploads/2012/11/20121129_185815.jpg"),
                  locale: Matchers.like({
                  address:Matchers.string("23-25 Great George Street, Leeds LS1 3BB"),
                  latitude:Matchers.decimal(53.8007317),
                  longitude:Matchers.decimal(-1.5481764)
                  }),
                  phoneNumber:Matchers.string("0113 220 4389"),
                  ratings: Matchers.eachLike({
                    name:Matchers.string(),
                    value:Matchers.integer()
                  }),
                  twitter:Matchers.string("EscobarLeeds"),
                  tags: Matchers.eachLike(Matchers.string())
                })
              })
            }
          }
        });
      
        await pubService.GetPubs({pageNumber:1, pageSize:25});
      });
    });
  })
