import React, { FC } from 'react';
//@ts-ignore
import CardList from './PubCardList.tsx'
//@ts-ignore
import useFetchPubs  from '../hooks/useFetchPubs.ts';

const Main: FC = () => {

  const {error, isLoading, data } =useFetchPubs();
  
  return (
    <div className="App">
      <h1>Pub Finder General</h1>
      <div>
        {(isLoading ) && (
            <h3>Loading departments mappings...</h3>
        )}
        {error && (
            <h3 color="error">
            Failed to fetch departments mappings
            </h3>
        )}
        { !isLoading && <CardList PubJsonList={data}/> }
      </div>
    </div>
  );
}
export default Main;