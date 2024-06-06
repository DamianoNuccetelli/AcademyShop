import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBox } from '@fortawesome/free-solid-svg-icons';
import './ProductCard.css';
import DeleteProduct from '../ProductCard/DeleteProduct/DeleteProduct';
import EditProduct from '../ProductCard/EditProduct/EditProduct';

const ProductCard = ({ id, nome, descrizione, quantità, productsData, setProductsData }) => {
    return (
        <div className='card_container'>
            <div className='box_card_container'>
                <div className="product_icon_box">
                    <FontAwesomeIcon icon={faBox} className='product_icon' />
                    <h3>{nome}</h3>
                </div>
                <div className='description'>
                    <p className='mb20'><strong>Descrizione:</strong> {descrizione} </p>
                </div>
                <div className='quantity'>
                    <div className='crud_button'>
                        <p><strong>Quantità:</strong> {quantità}</p>
                        <div className='ml_20'>
                            <EditProduct 
                                id={id} 
                                nome={nome} 
                                descrizione={descrizione} 
                                quantità={quantità} 
                                productsData={productsData} 
                                setProductsData={setProductsData} 
                            />
                            <DeleteProduct 
                                id={id} 
                                productsData={productsData} 
                                setProductsData={setProductsData} 
                            />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default ProductCard;
