import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import Navbar from '../Navbar/Navbar';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import './Ordine.css';

const Ordine = () => {
    return (
        <>
        <div className='container-fluid p-0'>
            <div className='d-flex'>
            <Navbar/>
            <div className='row mt-3 mx-3'>
                <div className='row'>
                    <div className='col-10' >
                    <div className='title_container'>
                        <h1>Ordini</h1>
                        <h2>Sezione Ordini</h2>
                    </div>
                    </div>
                    <div className='col-2'>
                        <div className='d-flex justify-content-end'>
                         <h3>Logo</h3>
                        </div>
                    </div>
                </div>
                <div className='row'>
                    <div className='col-10'>
                        <div className='d-flex justify-content-between my-2'>
                            <div className='add_container'>
                                <h2>Aggiungi Ordine</h2>
                                <FontAwesomeIcon icon={faPlus} className="plus-icon" />
                            </div>
                            <div className='d-flex align-items-end'>
                                <button className='btn purple-color' >Filtri</button>
                            </div>
                        </div>
                    <table class="table table-bordered border-primary table-striped shadow">
                        <thead>
                            <tr>
                            <th scope="col">Prodotto</th>
                            <th scope="col">Descrizione</th>
                            <th scope="col">Registrazione</th>
                            <th scope="col">Qt.</th>
                            <th scope="col">Aggiornamento</th>
                            <th scope="col">Stato</th>
                            <th scope="col">Azioni</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                            <th scope="row">Nome Prodotto</th>
                            <td>Mark</td>
                            <td>Otto</td>
                            <td>10</td>
                            <td>do</td>
                            <td>do</td>
                            <td>do</td>
                            </tr>
                            <tr>
                            <th scope="row">Nome Prodotto</th>
                            <td>Jacob</td>
                            <td>Thornton</td>
                            <td>15</td>
                            <td>do</td>
                            <td>do</td>
                            <td>do</td>
                            </tr>
                            <tr>
                            <th scope="row">Nome Prodotto</th>
                            <td>Larry the Bird</td>
                            <td>ciao</td>
                            <td>20</td>
                            <td>do</td>
                            <td>do</td>
                            <td>do</td>
                            </tr>
                        </tbody>
                        </table>
                    </div>
                        <div className='col-2'>
                            <div className='d-flex justify-content-center'>
                            <h3>Immagine</h3>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
        </div>
        </>
      
    );
};

export default Ordine;


