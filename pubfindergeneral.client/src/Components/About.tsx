import * as React from 'react';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText';
import DialogTitle from '@mui/material/DialogTitle';
import Dialog from '@mui/material/Dialog';
import { IAboutProps } from 'src/Types/types';
import { DialogContentText, Link, Typography } from '@mui/material';
import { styled } from '@mui/material/styles';
import Box from '@mui/material/Box';
import Rating from '@mui/material/Rating';

const aboutStyle = styled('div')(({ theme }) => ({
    backgroundColor: theme.palette.background.paper,
  }));


const About = (props: IAboutProps) => {
  const { onClose, selectedValue, open, name } = props;

  const handleClose = () => {
    onClose(selectedValue);
  };

  return (
    <Dialog onClose={handleClose} open={open}>
      <DialogTitle>{name}</DialogTitle>
      <DialogContentText id="alert-dialog-description">
        {(
            selectedValue && 
            <List sx={{ pt: 3, typography: 'body1', padding: 3}}>
                <ListItemText><Typography>Website: <Link href="#">{selectedValue?.website}</Link></Typography></ListItemText>
                <ListItemText><Typography>Twitter: <Link href="#">{selectedValue?.twitter}</Link></Typography></ListItemText>
                <ListItem>
                <Box
                    sx={{
                        '& > legend': { mt: 2 },
                    }}>
                    {( selectedValue.ratings && 
                        <List sx={{ pt: 1}}>
                            {selectedValue.ratings?.map(
                                ({
                                    name,
                                    value
                                }) => (
                                    <ListItem>
                                        <Typography component="legend">{name}</Typography>
                                        <Rating name="half-rating-read" defaultValue={value} precision={0.5} readOnly/>
                                    </ListItem>
                                )
                            )};
                        </List>
                    )}
                    </Box>
                </ListItem>
            </List>
        )}     
        </DialogContentText>
    </Dialog>
  );
}
export default About;