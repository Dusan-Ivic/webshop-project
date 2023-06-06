import React, { useEffect } from "react";
import Spinner from "react-bootstrap/Spinner";
import RegisterForm from "../../components/User/RegisterForm";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { registerUser, resetState } from "../../features/auth/authSlice";
import { registerRequestDto } from "../../models/registerDto";

const Register = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const authState = useSelector((state) => state.auth);

  useEffect(() => {
    if (authState.isError) {
      console.error(authState.message);
    }

    if (authState.isSuccess) {
      console.log(authState.user);
      navigate("/");
    }

    dispatch(resetState());
  }, [authState, navigate, dispatch]);

  const handleRegister = (data) => {
    const dto = registerRequestDto(data);
    dispatch(registerUser(dto));
  };

  if (authState.isLoading) {
    return (
      <Spinner animation="border" role="status">
        <span className="visually-hidden">Loading...</span>
      </Spinner>
    );
  }

  return (
    <div className="mb-5">
      <h1>Register</h1>
      <RegisterForm handleRegister={handleRegister} />
    </div>
  );
};

export default Register;