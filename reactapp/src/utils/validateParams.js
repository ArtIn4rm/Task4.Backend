export const validate = (isLogin, params) => {
    if(params.email === "" || !params.email.match(/^[^@\s]+@[^@\s]+\.[^@\s]+$/)){
        throw new Error("Email is not correct")
    }
    if(params.password === ""){
        throw new Error("Password is not entered")
    }
    if(!isLogin && !params.name.match(/[A-Za-z]([A-Za-z\-|_0-9])*/)){
        throw new Error("Username is not correct. Examples: User123, user_best654, funny456_786-0")
    }
}