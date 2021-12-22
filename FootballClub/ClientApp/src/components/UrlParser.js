export function getEntityNameFromUrlForCardPage() {
    return window.location.pathname.replace("CardPage", "").replace("/", "");
}

export function getEntityIdFromUrlForCardPage() {
    return window.location.search.replace("?id=", "");
}