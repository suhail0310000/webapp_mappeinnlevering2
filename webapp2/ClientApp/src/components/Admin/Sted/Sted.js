import React, { useState, useEffect } from 'react';
import axios from "axios";
import { Table } from 'react-bootstrap';
import CreateSted from './CreateSted';
import SlettSted from './SlettSted';

export default function Sted() {
    const [steder, setSteder] = useState([]);

    useEffect(() => {
        async function fetchSteder() {
            console.log('Fetch...');
            await axios.get('Kunde/GetAllSteder')
                .then(function (response) {
                    // handle success
                    console.log(response.data);
                    setSteder(response.data);
                })
                .catch(function (error) {
                    // handle error
                    console.log(error);
                })
                .then(function () {
                    // always executed
                });
        }
        fetchSteder();
    }, [])

    /*
    const deleteReise = (id) => {
        setReiser(reiser.filter(reise => reise.rId !== id))
    }

    const oppdatereReise = (oppdatertReise) => {
        const newList = reiser.map((item) => {
            if (item.rId === oppdatertReise.rId) {
                return oppdatertReise;
            }
            return item;
        });

        setReiser(newList);
    }
    */

    const createSted = (nySted) => {
        console.log(nySted);
        setSteder([...steder, nySted]);
    }

    const deleteSted = (id) => {
        setSteder(steder.filter(sted => sted.sId !== id))
    }

    return (
        <div>
            <CreateSted createSted={createSted} />
            <Table striped bordered hover style={{ maxWidth: '50%' }}>
                <thead>
                    <tr>
                        <th>#id</th>
                        <th>Sted</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        steder.length > 0 && steder.map((sted) => (
                            <tr>
                                <th scope="row">{sted.sId}</th>
                                <td>{sted.stedsNavn}</td>
                                <td width={"10%"}><SlettSted id={sted.sId} deleteSted={deleteSted} /></td>
                            </tr>
                        ))
                    }
                </tbody>
            </Table>
               
        </div>
    )
}