export const parseDate = (date) => { 
    return date.replace("T", " ").substr(0, 16)
}