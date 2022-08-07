import React, { FC } from 'react';
import { Card, Button } from 'react-bootstrap';
import { IPubCardProps } from '../Types/types';

const PubCard:FC<IPubCardProps> = ({
    Name,
    Excerpt,
    Thumbnail
}) =>{
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
                <Button variant="primary">Show More</Button>
                </Card.Body>
            </Card>
          </React.Fragment>
    );
}
export default PubCard;