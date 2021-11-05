import React, { useState, useEffect } from 'react';
import axios from "axios";
import { Button, Modal, Form } from 'react-bootstrap';
import { BsPencilSquare } from 'react-icons/bs';

export default function UpdateKunde(props) {
    const [show, setShow] = useState(false);
    const [values, setValues] = useState(props.kunde);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const handleTextfield = name => event => {
        let value = event.target.value;    
        setValues({ ...values, [name]:value })
    }

    const updateKunde = async () => {

        const oppdatertKunde = {
            kId: values.kId,
            fornavn: values.fornavn,
            etternavn: values.etternavn
        }
        console.log(oppdatertKunde);
        
        await axios.put('Kunde/EndreKunde', oppdatertKunde)
            .then(function (response) {
                // handle success
                console.log(response);
                if (response.data === "Kunden er nå oppdatert!") {
                    props.oppdatereKunde(oppdatertKunde)
                    handleClose();
                }
            })
            .catch(function (error) {
                // handle error
                console.log(error);
            })
            .then(function () {
                // always executed
            });
       
    }


    return (
        <div>
            <Button variant="primary" onClick={handleShow}>
                <BsPencilSquare />
            </Button>
            <Modal animation={false} show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Oppdatere kunde</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group className="mb-3">
                            <Form.Label>Fornavn</Form.Label>
                            <Form.Control type="text" placeholder="Fornavn" value={values.fornavn} onChange={handleTextfield('fornavn')} />
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Etternavn</Form.Label>
                            <Form.Control type="text" placeholder="Etternavn" value={values.etternavn} onChange={handleTextfield('etternavn')} />
                        </Form.Group>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Nei
                    </Button>
                    <Button variant="primary" onClick={updateKunde}>
                        Ja
                    </Button>
                </Modal.Footer>
            </Modal>
        </div>
    )
}