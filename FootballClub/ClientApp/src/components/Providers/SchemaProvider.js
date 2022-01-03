export async function getSchema(entityName) {
    validateEntityName(entityName);

    let response = await fetch(`/EntitySchema/GetRuEntitySchema?entityName=${entityName}`);
    return await response.json();
}

export function validateEntityName(entityName) {
    if (!entityName)
        throw new Error("Entity name can not be defined");
}