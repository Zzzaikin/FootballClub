
export async function getEntity(entityName, entityId) {
    validateEntityNameAndId(entityName, entityId);
    return await getEntityFromDatabase(entityName, entityId);
}

async function getEntityFromDatabase(entityName, entityId) {
    validateEntityNameAndId(entityName, entityId);

    let response = await fetch(`Data/Get${entityName}ById?id=${entityId}`);
    return await response.json();
}

function validateEntityNameAndId(entityName, entityId) {
    if (!entityName)
        throw new Error("Entity name can not be defined");

    if (!entityId)
        throw new Error("Entity identifier can not be defined");
}