import { useState, FC } from "react";
import {
  Box,
  TextField,
  Button,
  Typography,
  Paper,
} from "@mui/material";
import "./Login.css";

const Login: FC = () => {
  const [form, setForm] = useState({
    email: "",
    password: "",
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm({
      ...form,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    console.log("Login data:", form);
  };

  return (
    <Box className="login-page">
      <Paper elevation={4} className="login-card">
        <Typography variant="h4" className="login-title">
          Welcome Back
        </Typography>

        <Typography variant="subtitle1" className="login-subtitle">
          Please sign in to continue
        </Typography>

        <form className="login-form" onSubmit={handleSubmit}>
          <TextField
            label="Email Address"
            name="email"
            type="email"
            required
            fullWidth
            value={form.email}
            onChange={handleChange}
            margin="normal"
          />

          <TextField
            label="Password"
            name="password"
            type="password"
            required
            fullWidth
            value={form.password}
            onChange={handleChange}
            margin="normal"
          />

          <Button
            type="submit"
            variant="contained"
            fullWidth
            size="large"
            className="login-button"
          >
            Login
          </Button>

          <Typography className="register-text">
            Don’t have an account?{" "}
            <a href="/register" className="register-link">
              Create one
            </a>
          </Typography>
        </form>
      </Paper>
    </Box>
  );
};

export default Login;
