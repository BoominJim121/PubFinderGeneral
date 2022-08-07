import React, { FC, useState, useEffect } from 'react';
import { IPublicHouse, IPubs } from '../Types/types';
//@ts-ignore
import PubCard  from './PubCard.tsx';

const CardList: FC<IPubs> = ({
    PubJsonList
}) =>{

    const [data, setData] = useState<IPublicHouse[]>([]);

    useEffect(() => {
        const loadPubs = (): Promise<IPublicHouse[]> => {
            const hmm: IPublicHouse[] = PubJsonList as IPublicHouse[];
            return new Promise((res) => res(hmm));
          };
        const timer = setTimeout(() =>{
            loadPubs().then((res) => {
                setData(res);
            });
        }, Math.random() * 1000);
        return () => clearTimeout(timer);
    },[PubJsonList]);

    return (
        <div className="d-flex justify-content-around">
            <div className="card-columns">
                {(
                    data && data?.map(
                        ({
                            name, 
                            excerpt,
                            about
                        }) =>(
                            <PubCard
                                key= {name}
                                Name={name}
                                Excerpt ={excerpt?? ''}
                                Thumbnail={about?.thumbnail ?? ''}
                                />
                        )
                    )
                )}
            </div>
        </div>
    )
}

export default CardList;