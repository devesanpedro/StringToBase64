import React, { Component } from 'react';
import stringToBase64 from "../services/string-converter.service";
import * as signalR from '@microsoft/signalr';

class StringToBase64 extends Component {
 
    constructor(props) {
        super(props);
        this.state = { 
            inputText: "", 
            outputText: "", 
            error:"", 
            loading: false,
            connection: null,
            setConnection: null,
        };
        
        this.onNotifReceived = this.onNotifReceived.bind(this);
      }

    componentDidMount() {
        const protocol = new signalR.JsonHubProtocol();

        const transport = signalR.HttpTransportType.WebSockets;

        const options = {
        transport,
        logMessageContent: true,
        logger: signalR.LogLevel.Trace,
        // accessTokenFactory: () => this.props.accessToken,
        };

         // create the connection instance
        this.connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:7252/mainHub", options)
        .withHubProtocol(protocol)
        .build();

        this.connection.on('DatabaseOperation', this.onNotifReceived);
        this.connection.on('DownloadSession', this.onNotifReceived);
        this.connection.on('UploadSession', this.onNotifReceived);
        this.connection.on('ReceiveMessage', this.onNotifReceived);

        this.connection.start()
        .then(() => console.info('SignalR Connected'))
        .catch(err => console.error('SignalR Connection Error: ', err));
    }

    componentWillUnmount () {
        this.connection.stop();
      }

    onNotifReceived (res) {
        const { outputText } = this.state;

        var returnValue = outputText + res;

        this.setState({ outputText: returnValue});
        
        console.info('Received Message:', outputText);
      }

    render() { 
        const {
            inputText,
            outputText,
            loading
        } = this.state;
        console.log(outputText);
        return (<div>

            <div className='row'>
                <div className='col-lg-12'>
                <div class="card">
                    <div class="card-header">
                    Convert String to Base64
                    </div>
                    <div class="card-body">
                        <div className='col-lg-6'>
                            <form>
                        <div class="mb-3">
                            <label for="inputText" class="form-label">Input text</label>
                            <input type="text" class="form-control" id="inputText" value={inputText} onChange={(e) => {this.handleTextChange(e) }}/>
                        </div>
                        
                        <div className='text-end'>
                            { loading == true ?
                                    <div class="d-flex justify-content-center">
                                    <div class="spinner-border" role="status">
                                        <span class="visually-hidden">Loading...</span>
                                    </div>
                                </div> : ""
                            }

                            <button type="button" class="btn btn-primary me-1" onClick={this.handleConvert} disabled={loading}>Convert</button>
                            <button type="button" class="btn btn-secondary me-1">Cancel</button>
                            <button type="button" class="btn btn-danger" onClick={this.handleClear}>Clear</button>
                        </div>
                        <div class="mb-3">
                            <label for="outputText" class="form-label">Output (Base64 String)</label>
                            <input type="text" class="form-control" id="outputText" value={outputText || ""} />
                        </div>
                    </form>
                        </div>
                        <div className='col-lg-6'></div>
                    </div>
                </div>
                    
                </div>
            </div>
        </div>);
    }

    handleConvert = () => {
        var { inputText, outputText, loading } = this.state;

        if (inputText != null) {
            this.convertStringToBase64(inputText);
        }
    }

    handleTextChange = (e) => {
        this.setState({inputText: e.target.value});
    }

    handleClear = () => {
        this.setState({outputText: "" });
    }

    async convertStringToBase64(inputText) {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ text: inputText })
        };
        this.setState({ loading: true });
        await fetch('https://localhost:7252/home/stringtobase64', requestOptions)
        .then(async response => {
            const data = await response.json();
            this.setState({ loading: false });
            // this.setState({ outputText: data.data, loading: false });
        }).catch(error => {
            this.setState({ error: error.message });
        })
      } 
}

export default StringToBase64;