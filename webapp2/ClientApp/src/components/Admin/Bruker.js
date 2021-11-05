import React, { useState, useEffect } from 'react';
import axios from "axios";
import { Table } from 'react-bootstrap';

export default function Kunde() {
    const [bruker, setBruker] = useState([]);

    useEffect(() => {
        async function fetchSteder() {
            console.log('Fetch...');
            await axios.get('Kunde/GetAlleBrukere')
                .then(function (response) {
                    // handle success
                    console.log(response.data);
                    setBruker(response.data);
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

    const lagNyReise = (nyReise) => {
        console.log(nyReise);
        setReiser([...reiser, nyReise]);
    }
    */


    return (
        <div>
            <Table striped>
                <thead>
                    <tr>
                        <th>#id</th>
                        <th>Brukernavn</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        bruker.length > 0 && bruker.map((b) => (
                            <tr>
                                <th scope="row">{b.id}</th>
                                <td>{b.brukernavn}</td>
                            </tr>
                        ))
                    }
                </tbody>
            </Table>
        </div>
    )
}