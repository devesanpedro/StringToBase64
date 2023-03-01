import { requests } from "./request-factory";

export default {
    stringToBase64: (data) => requests.post("home/stringtobase64", data),
    base64ToString: (data) => requests.post("home/base64tostring", data)
}