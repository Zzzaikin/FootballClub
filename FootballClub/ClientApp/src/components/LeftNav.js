import React, { useState } from 'react';

function LeftNav() {
    const [turnButtonCaption, setTurnButtonCaption] = useState(">");
    const [wrapperRef, setWrapperRef] = useState(React.createRef());

    function onTurnButtonClick(e) {
        const caption = turnButtonCaption === ">" ? "<" : ">";
        setTurnButtonCaption(caption);

        const wrapper = wrapperRef.current;
        wrapper.classList.toggle('is-nav-open');
    }

    return (
        <div className="d-flex p-2 bd-highlight">
            <div ref={wrapperRef} className="wrapper">
                <div className="p-2 bd-highlight" style={{ "width": "10%" }}>
                    <div class="nav flex-column nav-pills" id="v-pills-tab" role="tablist" aria-orientation="vertical">
                        <a class="mt-1 nav-link active" id="v-pills-home-tab" data-toggle="pill" href="#v-pills-home" role="tab" aria-controls="v-pills-home" aria-selected="true">Игроки</a>
                        <a class="nav-link" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab" aria-controls="v-pills-profile" aria-selected="false">Тренеры</a>
                        <a class="nav-link" id="v-pills-messages-tab" data-toggle="pill" href="#v-pills-messages" role="tab" aria-controls="v-pills-messages" aria-selected="false">Менеджеры игроков</a>
                        <a class="nav-link" id="v-pills-settings-tab" data-toggle="pill" href="#v-pills-settings" role="tab" aria-controls="v-pills-settings" aria-selected="false">Взыскания</a>
                        <a class="nav-link" id="v-pills-settings-tab" data-toggle="pill" href="#v-pills-settings" role="tab" aria-controls="v-pills-settings" aria-selected="false">Матчи</a>
                        <a class="nav-link" id="v-pills-settings-tab" data-toggle="pill" href="#v-pills-settings" role="tab" aria-controls="v-pills-settings" aria-selected="false">Дисквалификации</a>
                    </div>
                    <div className="lowerButtons">
                        <button type="button" className="btn btn-outline-dark"
                            style={{ "marginRight": "90px" }}>Выход</button>
                        <button type="button" className="btn btn-outline-info"
                            style={{ "marginLeft": "50px" }} onClick={onTurnButtonClick}>{turnButtonCaption}</button>
                    </div>
                </div>
            </div>
            <div className="p-2 bd-highlight">
                <div class="tab-content" id="v-pills-tabContent">
                    <div class="tab-pane fade show active" id="v-pills-home" role="tabpanel" aria-labelledby="v-pills-home-tab">Первая вкладка</div>
                    <div class="tab-pane fade" id="v-pills-profile" role="tabpanel" aria-labelledby="v-pills-profile-tab">Вторая вкладка.</div>
                    <div class="tab-pane fade" id="v-pills-messages" role="tabpanel" aria-labelledby="v-pills-messages-tab">...</div>
                    <div class="tab-pane fade" id="v-pills-settings" role="tabpanel" aria-labelledby="v-pills-settings-tab">...</div>
                </div>
            </div>
        </div>
    );
}

export default LeftNav;