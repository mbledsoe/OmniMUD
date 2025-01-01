'use strict';

class OmniMud {
    constructor(output, inputForm, commandInput) {
        this.output = output;
        this.inputForm = inputForm;
        this.commandInput = commandInput;
    }

    updateHandler(message) {
        console.log('updateHandler', message);

        const newOutputMessage = document.createElement('div');
        newOutputMessage.classList.add('output-message');
        newOutputMessage.innerHTML = message;

        this.output.appendChild(newOutputMessage);
    }

    signalRStarted() {
        console.log('signalr started');
    }

    signalRError(error) {
        console.error(error.toString());
    }

    startSignalR() {
        this.connection = new signalR.HubConnectionBuilder().withUrl("/OmniMudHub").build();

        this.connection.on("Update", (message) => this.updateHandler(message));

        this.connection.start()
            .then(this.signalRStarted)
            .catch(this.signalRError);
    }

    inputFormSubmitted(ev) {
        ev.preventDefault();

        const command = this.commandInput.value;
        console.log('commandText', command);

        this.connection.invoke('SendCommand', command)
            .catch(this.signalRError);

        this.commandInput.value = '';
    }

    run() {
        this.startSignalR();
        this.inputForm.addEventListener('submit', (ev) => this.inputFormSubmitted(ev));
    }
}