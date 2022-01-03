
export async function getEntity(entityName, entityId) {
    validateEntityNameAndId(entityName, entityId);
    return await getEntityFromDatabase(entityName, entityId);
}

export async function getEmptyEntity(entityName) {
    validateEntityName(entityName);

    let response = await fetch(`${entityName}/GetEmptyEntity`);
    return await response.json();
}

async function getEntityFromDatabase(entityName, entityId) {
    validateEntityNameAndId(entityName, entityId);

    let response = await fetch(`${entityName}/GetEntityById?id=${entityId}`);
    return await response.json();
}

function validateEntityNameAndId(entityName, entityId) {
    validateEntityName(entityName);

    if (!entityId)
        throw new Error("Entity identifier can not be defined");
}

function validateEntityName(entityName) {
    if (!entityName)
        throw new Error("Entity name can not be defined");
}