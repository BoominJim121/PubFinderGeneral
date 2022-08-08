import React, { FC, useState, useEffect } from 'react';
import CardList from './PubCardList'
import useFetchPubs  from '../hooks/useFetchPubs';
import {  Box, Divider, Pagination, Grid, Select, MenuItem, SelectChangeEvent, Typography } from '@mui/material';
const Main: FC = () => {
  
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(25);
  const{ data, isLoading, error, refetch} = useFetchPubs({pageNumber: page, pageSize:pageSize})

  useEffect(()=>{
    refetch();
  },[page, pageSize])

  const handleChange = (event: React.ChangeEvent<unknown>, value: number) => {
   setPage(value);
  };
  const handlePageSizeChange = (event: SelectChangeEvent) => {
    var y: number = +event.target.value;
    setPageSize(y);
  };

  return (
    <div className="App">
      <h1>Pub Finder General</h1>
      <Box sx={{ boxShadow: 2, m: 4.5, borderRadius: 2 }}>
        <Grid
                container
                direction="row"
                justifyContent="center"
                alignItems="center"
            >  
          <Typography variant="h6" > Navigation :-</Typography> 
          <Pagination color= "primary" count={data?.totalPages} page={page} onChange={handleChange} />
          <Select sx ={{ margin:2}}
            labelId="pageSize-label"
            id="pagesize-Select"
            value={pageSize.toString()}
            label="Page Size"
            onChange={handlePageSizeChange}
          >
            <MenuItem value={5}>5</MenuItem>
            <MenuItem value={10}>10</MenuItem>
            <MenuItem value={15}>15</MenuItem>
            <MenuItem value={20}>20</MenuItem>
            <MenuItem value={25}>25</MenuItem>
            <MenuItem value={30}>30</MenuItem>
            <MenuItem value={40}>40</MenuItem>
            <MenuItem value={50}>50</MenuItem>
          </Select>
        </Grid>
      </Box>
      <Box sx ={{ boxShadow: 2, m: 2, borderRadius: 2}}>
        {(isLoading ) && (
            <h3>Loading departments mappings...</h3>
        )}
        {error && (
            <h3 color="error">
            Failed to fetch departments mappings
            </h3>
        )}
        { !isLoading && <CardList publicHouses={data!}/> }
      </Box>

    </div>
  );
}
export default Main;


