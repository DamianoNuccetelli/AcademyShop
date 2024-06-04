import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBox } from '@fortawesome/free-solid-svg-icons'; // Esempio di un'icona casuale, puoi sostituirla con l'icona che desideri
import './ProductCard.css';

const ProductCard = ({ name, price }) => {
    return (
        <div className='card_container'>
            <div className="product_icon_box">
                <FontAwesomeIcon icon={faBox} className='product_icon'/>
                <h3>{name}</h3>
            </div>
            <div className='description'>
                <p className='mb20'><strong>Descrizione:</strong> Lorem Ipsum è un testo segnaposto utilizzato nel settore della tipografia e della stampa.</p>
                <div className='crud_button'>
                    <p><strong>Quantità</strong>: 2</p>
                    <div className='buttons'>
                        <FontAwesomeIcon icon={faBox}/>
                        <FontAwesomeIcon icon={faBox}/>
                    </div>
                </div>
            </div>

        </div>
    );
};

export default ProductCard;
