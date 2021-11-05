import React, { useState, useEffect } from 'react';
import axios from "axios";
import { Button, Modal } from 'react-bootstrap';
import { AiFillDelete } from 'react-icons/ai';

export default function SlettSted(props) {
    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const slettSted = async () => {
        console.log(props.id);
        let id = props.id;
        /*
        await axios.delete('Kunde/SlettReise/' + props.id)
            .then(function (response) {
                // handle success
                console.log(response);
                if (response.data) {
                    props.deleteSted(id);
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
           */
    }
    return (
        <div>
            <Button variant="danger" onClick={handleShow}>
                <AiFillDelete />
            </Button>
            <Modal animation={false} show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Slett sted</Modal.Title>
                </Modal.Header>
                <Modal.Body>Er du sikker på at du vil slette sted?</Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Nei
                    </Button>
                    <Button variant="primary" onClick={slettSted}>
                        Ja
                    </Button>
                </Modal.Footer>
            </Modal>
        </div>
    )
}