export async function getSchema(schemaName) {
    if (!schemaName)
        throw new Error("Schema name can not be defined");

    let response = await fetch(`/Data/GetRuEntitySchema?entityName=${schemaName}`);
    return await response.json();
}