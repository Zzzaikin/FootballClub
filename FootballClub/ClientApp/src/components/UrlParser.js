export function getEntityNameFromUrlForCardPage() {
    const pathname = window.location.pathname;
    const cardPage = "CardPage";
    const insert = "/Insert";

    if (pathname.endsWith(cardPage))
        return pathname.replace(cardPage, "").replace("/", "");

    if (pathname.startsWith(insert))
        return pathname.replace(insert, "").replace("/", "");
}

export function getEntityIdFromUrlForCardPage() {
    return window.location.search.replace("?id=", "");
}