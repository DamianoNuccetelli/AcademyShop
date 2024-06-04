import React, {useState, useEffect} from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import '../../Header/Header.css';
import banner from '../../../img/banner.png';




const ContentOrdine = () => {
    const [orders, setOrders] = useState([]);
const userId = localStorage.getItem('userId');

  useEffect(() => {
    const fetchOrders = async () => {

      const API_URL = `https://localhost:7031/orders?userId=${userId}`;
      try {

        const response = await fetch(API_URL);
        if (response.ok) {
          const data = await response.json();
          setOrders(data);
        } else {
          console.error('Error fetching orders:', response.status);
        }
      } catch (error) {
        console.error('Error:', error);
      }
    };

    fetchOrders();
  }, []);
 
    return (
        <div className="header">
            <div className='title_container'>
                <h1>Ordini</h1>
                <h2>Ordini di Mario</h2>
            </div>
            <div className='welcome_container'>
                <div className='add_container'>
                    {/* <h2>Nuovo ordine</h2> */}
                    <FontAwesomeIcon icon={faPlus} className="plus-icon" />
                </div>
                <div className='banner_container'>
                    {/* <img src={banner} alt="Logo" /> */}
                </div>
            </div>
            <div className='products_container'>
            <h2>Tutti i prodotti</h2>

                <table>
                    <thead>
                        <tr>
                            <th>Nome Prodotto</th>
                            <th>Descrizione Prodotto</th>
                            <th>Stato Ordine</th>
                            <th>Quantità</th>
                            <th>Id Prodotto</th>
                            <th>Data Registrazione</th>
                            <th>Data Aggiornamento</th>
                            <th>Azioni</th>
                        </tr>
                    </thead>
                    <tbody>
            {orders.map((order, index) => (
              <tr key={index}>
                <td>{order.prodottoNome}</td>
                <td>{order.prodottoDescrizione}</td>
                <td>{order.statoOrdineDescrizione}</td>
                <td>{order.quantita}</td>
                <td>{order.prodottoId}</td>
                <td>{new Date(order.dataRegistrazione).toLocaleDateString()}</td>
                <td>{new Date(order.dataAggiornamento).toLocaleDateString()}</td>
                <td>
                    <button>Delete</button>
                    <button>Show</button>
                    <button>Edite</button>
                    </td>
              </tr>
            ))}
          </tbody>
                    
                    
                </table>
            </div>
        </div>
    );
};

export default ContentOrdine;


