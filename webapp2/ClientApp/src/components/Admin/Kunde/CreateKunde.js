import React, { useState, useEffect } from 'react';
import axios from "axios";
import { Button, Form, Modal } from 'react-bootstrap';
import { AiOutlinePlusCircle } from 'react-icons/ai';

export default function CreateKunde(props) {
    const [show, setShow] = useState(false);
    const [fornavn, setFornavn] = useState('');
    const [etternavn, setEtternavn] = useState('');
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const clickSumbmit = async () => {
        const nyKunde = {
            fornavn: fornavn,
            etternavn: etternavn
        }
        console.log(nyKunde);

        await axios.post('Kunde/OpprettKunde', nyKunde)
            .then(function (response) {
                // handle success
                console.log(response);
                if (response.data === "Kunden er nå opprettet") {
                    props.createKunde(nyKunde);
                }
            })
            .catch(function (error) {
                // handle error
                console.log(error);
            })
            .then(function () {
                // always executed
            });


        handleClose();
    }
    return (
        <div>
            <div onClick={handleShow} style={{ display: 'flex', justifyContent: 'flex-start', cursor: 'pointer', alignItems: 'center' }}>
                <h4>Opprett ny kunde</h4>
                <div>
                    <AiOutlinePlusCircle size={25} />
                </div>
            </div>

            <Modal animation={false} show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Ny kunde</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group className="mb-3">
                            <Form.Label>Fornavn</Form.Label>
                            <Form.Control type="text" placeholder="Fornavn" value={fornavn} onChange={(e) => setFornavn(e.target.value)} />
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Etternavn</Form.Label>
                            <Form.Control type="text" placeholder="Etternavn" value={etternavn} onChange={(e) => setEtternavn(e.target.value)} />
                        </Form.Group>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Cancel
                    </Button>
                    <Button variant="primary" onClick={clickSumbmit}>
                        Create
                    </Button>
                </Modal.Footer>
            </Modal>
        </div>
    )
}