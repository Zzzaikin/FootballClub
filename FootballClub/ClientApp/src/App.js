import React, { Component } from 'react';
import LeftNav from './components/LeftNav'

import './custom.css'

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <LeftNav />
        );
    }
}
