import React, { FC, useState, useEffect } from 'react';
import { IPubCardListProps, IPublicHouse } from '../Types/types';
import PubCard  from './PubCard';
import {Grid, Box} from '@material-ui/core';
const CardList: FC<IPubCardListProps> = ({
    publicHouses
}) =>{
    const [data, setData] = useState<IPublicHouse[]>();
    useEffect(() => {
        
        const loadPubs = (): Promise<IPublicHouse[]> => {
            const innerPubs: IPublicHouse[] = publicHouses.pubs  as IPublicHouse[]
            return new Promise((res) => res(innerPubs));
          };

        const timer = setTimeout(() =>{
            loadPubs().then((res) => {
                setData(res);
            });
        }, Math.random() * 1000);
        return () => clearTimeout(timer);
    }, [publicHouses]);
    return (
        <Box sx ={{ padding: 10}}>
            <Grid
                container
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
                                        key={name}
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
        </Box>
    )
}

export default CardList;