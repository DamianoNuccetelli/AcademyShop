import React, { useState, useEffect } from 'react';
import GetProducts from '../ProductCard/GetProducts/GetProducts';
import AddProduct from '../ProductCard/AddProduct/AddProduct';
import banner from '../../img/banner.png';
import './Header.css';

const Header = () => {
    const [productsData, setProductsData] = useState([]);
    const [userName, setUserName] = useState('');
    const [userCognome, setUserCognome] = useState('');

    const userId = sessionStorage.getItem('userId');
    

    useEffect(() => {
        const fetchUserName = async () => {
            try {
                const response = await fetch(`https://localhost:7031/users/${userId}`);
                const data = await response.json();
                setUserName(data.nome);
                setUserCognome(data.cognome);
                sessionStorage.setItem('nome', data.nome);
                sessionStorage.setItem('cognome', data.cognome);
            } catch (error) {
                console.error('Error fetching user data:', error);
            }
        };

        if (userId) {
            fetchUserName();
        }
    }, [userId]);

    const handleOnEndCreate = (flag) => {
        if (flag) {
        console.log("flag", flag);
        window.location.reload()
        }
      };

    return (
        <div className="header">
            <div className='title_container'>
                <div className='title_text'>
                    <h1>Dashboard</h1>
                    <h2>Benvenuto, {userName}</h2>
                </div>
            </div>
            <div className='welcome_container'>
                <AddProduct fetchProducts={() => setProductsData([])} onEndCreate= {handleOnEndCreate}/>
                <div className='banner_container'>
                    <img src={banner} alt="Banner" />
                </div>
            </div>
            <GetProducts productsData={productsData} setProductsData={setProductsData} />
        </div>
    );
};

export default Header;
