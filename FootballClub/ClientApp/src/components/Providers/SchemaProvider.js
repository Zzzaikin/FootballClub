export async function getSchema(entityName) {
    validateEntityName(entityName);

    let response = await fetch(`/EntitySchema/GetRuEntitySchema?entityName=${entityName}`);
    return await response.json();
}

export async function getReferencedEntity(entityName, columnName) {
    validateEntityName(entityName);

    if (!columnName)
        throw new Error("Column name can not be defined");

    let response = await fetch(`/EntitySchema/GetReferencedTableName?tableName=${entityName}&columnName=${columnName}`);
    return await response.text();
}

export function validateEntityName(entityName) {
    if (!entityName)
        throw new Error("Entity name can not be defined");
}