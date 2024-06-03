import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBox } from '@fortawesome/free-solid-svg-icons'; // Esempio di un'icona casuale, puoi sostituirla con l'icona che desideri
import './ProductCard.css';

const ProductCard = ({ name, price }) => {
    return (
        <div className='card_container'>
            <div className="product_icon">
                <FontAwesomeIcon icon={faBox} />
            </div>
            <div className="product_info">
                <p>{name}</p>
                <p>Price: ${price}</p>
            </div>
        </div>
    );
};

export default ProductCard;
