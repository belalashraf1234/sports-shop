export type Pagination<T>={
    pageId:number,
    pageSize:number,
    count:number,
    data:T[]
}