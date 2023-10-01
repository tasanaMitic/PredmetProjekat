import { post, get, remove } from './httpMethods'

const login = (body) => {
    const res =  post('/api/account/login', body)
    .then(function(res){
        return res;
    });
    console.log('login');
    //console.log(res);
}

export default login;