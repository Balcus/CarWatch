import { useState, FC } from "react";
import { Box, TextField, Button, Typography, Paper } from "@mui/material";
import "./Login.css";

const Login: FC = () => {
  const [form, setForm] = useState({
    email: "",
    password: "",
  });

  const [errors, setErrors] = useState({
    email: "",
    password: "",
  });

  const validateEmail = (email: string) => {
    const pattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return pattern.test(email);
  };

  const validate = () => {
    let valid = true;
    const newErrors = { email: "", password: "" };

    if (!form.email) {
      newErrors.email = "Email is required";
      valid = false;
    } else if (!validateEmail(form.email)) {
      newErrors.email = "Invalid email format";
      valid = false;
    }

    if (!form.password) {
      newErrors.password = "Password is required";
      valid = false;
    } else if (form.password.length < 6) {
      newErrors.password = "Password must be at least 6 characters";
      valid = false;
    }

    setErrors(newErrors);
    return valid;
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value });
    setErrors({ ...errors, [e.target.name]: "" });
  };

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (!validate()) return;
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
            required
            fullWidth
            margin="normal"
            value={form.email}
            onChange={handleChange}
            error={Boolean(errors.email)}
            helperText={errors.email}
          />

          <TextField
            label="Password"
            name="password"
            type="password"
            required
            fullWidth
            margin="normal"
            value={form.password}
            onChange={handleChange}
            error={Boolean(errors.password)}
            helperText={errors.password}
          />

          <Button
            type="submit"
            variant="contained"
            fullWidth
            size="large"
            className="login-button"
          >
            🚗 Login
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
