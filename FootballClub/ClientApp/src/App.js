import React from 'react';
import { BrowserRouter, Route } from 'react-router-dom';
import Layout from './components/Layout'

import './custom.css'

function App() {
    return (
        <BrowserRouter>
            <Layout />
        </BrowserRouter>
    );
}

export default App;
