import React, { useState, useEffect } from 'react';
import axios from "axios";
import { Button, Form, Modal } from 'react-bootstrap';
import { AiOutlinePlusCircle } from 'react-icons/ai';

export default function CreateSted(props) {
    const [show, setShow] = useState(false);
    const [sted, setSted] = useState('');
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const clickSumbmit = async () => {
        // Make request to the backend to create new reise object
        console.log(sted);
        
        const nySted = {
            stedsNavn: sted 
        }
        await axios.post('Kunde/OpprettSted', nySted)
            .then(function (response) {
                // handle success
                console.log(response);
                if (response.data === "Sted er nå opprettet") {
                    props.createSted(nySted);
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
                <h4>Opprett ny sted</h4>
                <div>
                    <AiOutlinePlusCircle size={25} />
                </div>
            </div>

            <Modal animation={false} show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Ny sted</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group className="mb-3">
                            <Form.Label>Sted</Form.Label>
                            <Form.Control type="text" placeholder="Ny sted" value={sted} onChange={(e) => setSted(e.target.value)} />
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