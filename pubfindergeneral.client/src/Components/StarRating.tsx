import React, { FC } from "react";
import { IRating } from "src/Types/types";


const StarRating: FC<IRating> = ({
    name: string, 
    value: number
}) => {  
    return (
        <div className="star-rating">
        {[...Array(5)].map((star) => {        
          return (         
            <span className="star">&#9733;</span>        
          );
        })}
      </div>
    );
}

export default StarRating;