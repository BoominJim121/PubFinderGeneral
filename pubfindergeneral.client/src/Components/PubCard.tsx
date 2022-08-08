import React, { FC } from 'react';
import { Card } from 'react-bootstrap';
import { IAbout, IPubCardProps } from '../Types/types';
import Button from '@mui/material/Button';
import  About  from './About';

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
            <Card style={{ width: '18rem' }}>
                <Card.Body>
                <Card.Img variant="top" src={Thumbnail} />
                <Card.Body>
                    <Card.Title>{Name}</Card.Title>
                    <Card.Text>
                        {Excerpt}
                    </Card.Text>
                </Card.Body>
                <Button variant="outlined" onClick={handleClickOpen}>
                    About...
                </Button>
                <About
                    selectedValue={AboutValue}
                    open={open}
                    onClose={handleClose}
                    name={Name}
                />
                </Card.Body>
            </Card>
          </React.Fragment>
    );
}
export default PubCard;