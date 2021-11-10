import React from 'react';
import { BrowserRouter, Route } from 'react-router-dom';
import Section from './components/Section';
import Layout from './components/Layout'
import Content from './components/Content';

import './custom.css'

function App() {
    return (
        <BrowserRouter>
            <Layout />
        </BrowserRouter>
    );
}

export default App;
