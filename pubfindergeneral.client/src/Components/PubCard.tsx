import React, { FC } from 'react';
import { IAbout, IPubCardProps } from '../Types/types';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Button from '@mui/material/Button';
import  About  from './About';
import { Grid, Typography } from '@mui/material';

const PubCard:FC<IPubCardProps> = ({
    Name,
    Excerpt,
    Thumbnail,
    AboutValue
}) =>{
    const [open, setOpen] = React.useState(false);
    const [selectedValue, setSelectedValue] = React.useState(AboutValue);
    const handleClickOpen = () => {
        setOpen(true);
      };
    
      const handleClose = (value: IAbout| undefined) => {
        setOpen(false);
        setSelectedValue(value);
      };

    return (
        <React.Fragment>
            <Card sx={{ padding: 3, width: '18rem', boxShadow: 2 , borderRadius: 2}}>
                <CardMedia sx={{ padding: 3}} component="img" image={Thumbnail} alt={Thumbnail} />
                <CardContent>
                    <Typography gutterBottom variant="h5" component="div">{Name}</Typography>
                    <Typography variant="body2" color="text.secondary">
                            {Excerpt}
                    </Typography>
                    <CardActions sx={{alignContent: 'center', padding: 1}}>
                        <Grid direction="row" >
                            <Button variant="outlined"  onClick={handleClickOpen}>...About</Button>
                            <About
                                selectedValue={AboutValue}
                                open={open}
                                onClose={handleClose}
                                name={Name}
                            />
                        </Grid>
                    </CardActions>
                </CardContent>
            </Card>
          </React.Fragment>
    );
}
export default PubCard;