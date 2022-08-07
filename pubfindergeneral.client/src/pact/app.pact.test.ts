const path = require('path');
const Pact = require('@pact-foundation/pact').Pact;
import { mocked } from 'ts-jest/utils';

const pactProvider = new Pact({
    consumer: 'integration-admin-web-reapit-installation-api-client',
    provider: 'ingest-admin-api',
    log: path.resolve(__dirname, '../../pact-logs', 'pact.log'),
    dir: path.resolve(__dirname, '../../../../../../../pacts')
  });