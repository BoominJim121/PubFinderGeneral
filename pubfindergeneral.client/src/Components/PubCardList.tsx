import React, { FC, useState, useEffect } from 'react';
import { IPublicHouse, IPubs } from '../Types/types';
import PubCard  from './PubCard';
import {Grid} from '@material-ui/core';
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
        <Grid container
        direction="row"
        justifyContent="center"
        alignItems="center"
      >          
                {(
                    data && data?.map(
                        ({
                            name, 
                            excerpt,
                            about
                        }) =>(
                            <Grid item xs={2}>
                                <PubCard
                                    key= {name}
                                    Name={name}
                                    Excerpt ={excerpt?? ''}
                                    Thumbnail={about?.thumbnail ?? ''}
                                    AboutValue={about}
                                    />
                            </Grid>
                        )
                    )
                )}
        </Grid>
    )
}

export default CardList;