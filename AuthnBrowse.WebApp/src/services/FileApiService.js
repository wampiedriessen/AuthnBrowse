import axios from 'axios';

export default class FileApiService {
    static IName = "FileApiService";

    constructor(props) {
        this._apiBaseUrl = props.ApiBaseUrl;
    }

    GetFiles = async () => {
        let response = await axios.get(this._apiBaseUrl );
        return response.data;
    };
}