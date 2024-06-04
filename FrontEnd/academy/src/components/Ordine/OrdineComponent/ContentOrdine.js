import React, {useState, useEffect} from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import '../../Header/Header.css';
import banner from '../../../img/banner.png';
import Modal from 'react-modal';




const ContentOrdine = () => {
  const [prodotti, setProdotti] = useState([]);
  const [quantità, setQuantità] = useState(1);

  const [modalIsOpen, setModalIsOpen] = useState(false);

    const openModal = () => {
        setModalIsOpen(true);
    };
        
    const closeModal = () => {
        setModalIsOpen(false);
    };
    const addOrdine = async(idUtente, idProdotto, quantitàProdotto)=>{
      closeModal();
      console.log("addOrdine: ",idUtente, idProdotto, quantitàProdotto)
      const API_URL = `https://localhost:7031/orders?idUtente=${idUtente}&idProdotto=${idProdotto}&quantit%C3%A0=${quantitàProdotto}`;
      try {
          const response = await fetch(API_URL, {
              method: 'POST',
              headers: {
                  'Content-Type': 'application/json',
              },
              body: JSON.stringify({
                idUtente: idUtente,
                idProdotto: idProdotto,
                quantità: quantitàProdotto,
              }),
          });
    
          if (response.ok) {
              console.log('Ordine aggiunto con successo', response.status);
          } else {
              console.error('Errore durante l\'aggiunta dell\'ordine:', response.status);
          }
      } catch (error) {
          console.error('Errore:', error);
      }
    }
    
    //-->
    const [selectedProduct, setSelectedProduct] = useState('');
    const [searchTerm, setSearchTerm] = useState('');
    //-->
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
  }, [addOrdine]);

  

  
  const getProducts = async () => {
    const API_URL = `https://localhost:7031/products?userId=${userId}`;
    try {
      const response = await fetch(API_URL);
      if (response.ok) {
        const data = await response.json();
        setProdotti(data);
        openModal(); // Call openModal after fetching products
      } else {
        console.error('Error fetching products:', response.status);
      }
    } catch (error) {
      console.error('Error:', error);
    }
  };

const handleProductChange = (event) => {
  setSelectedProduct(event.target.value);
};

const handleSearchChange = (event) => {
  setSearchTerm(event.target.value);
};

const filteredProducts = prodotti.filter(prodotti =>
  prodotti.nome.toLowerCase().includes(searchTerm.toLowerCase())
);

const handleQuantityChange = (event) => {
  setQuantità(parseInt(event.target.value) || 1); // Ensure quantity is a positive integer
};
    return (
        <div className="header">
            <div className='title_container_ordine'>
              <div className='title_text'>
                <h1>Ordini</h1>
                <h2>Ordini di Mario</h2>
              </div>
            </div>
            <div className='welcome_container_ordine'>
                <div className='add_container_ordine'>
                    {/* <h2>Nuovo ordine</h2> */}
<<<<<<< HEAD
                    <FontAwesomeIcon icon={faPlus} className="plus-icon" onClick={() => getProducts(userId)} />
=======
                    <FontAwesomeIcon icon={faPlus} className="plus-icon_ordine" />
>>>>>>> cb4f7c0952589b69a5f1e497813f9d029a3a29f3
                </div>
                <div className='banner_container'>
                    <img src={banner} alt="Logo" />
                </div>
            </div>
<<<<<<< HEAD
            <Modal
                isOpen={modalIsOpen}
                onRequestClose={closeModal}
                className="modal"
                overlayClassName="overlay"
            >
                <div className="popup-content">
                    <h2>Welcome to our Popup</h2>
                    <div>
            <input
                type="text"
                placeholder="Search product"
                value={searchTerm}
                onChange={handleSearchChange}
            />
             <div>
            <select id="product" value={selectedProduct} onChange={handleProductChange}>
                <option value="">Select a product</option>
                {filteredProducts.map((product, index) => (
                    <option key={index} value={product.id}>
                        {product.nome}  
                    </option>
                ))}
            </select>
            </div>
            <div>
            <input
                type="number"
                min="1"
                placeholder="Quantità"
                value={quantità}
                onChange={handleQuantityChange}
            /></div>
        </div>
        <button onClick={closeModal} className="close-button">Close</button> <button onClick={() => addOrdine(userId, selectedProduct, quantità)} className="close-button">Submit</button>
                </div>
            </Modal>
            <div className='products_container'>
=======
            <div className='products_container_ordine'>
>>>>>>> cb4f7c0952589b69a5f1e497813f9d029a3a29f3
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


