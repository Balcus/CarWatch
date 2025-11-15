import { FC, useState } from "react";
import { Box, TextField, Button, Typography, Paper } from "@mui/material";
import "./Register.css";

export const Register: FC = () => {
  const [form, setForm] = useState({
    name: "",
    email: "",
    password: "",
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    console.log("Register data:", form);
  };

  return (
    <Box className="register-page">
      <Paper elevation={3} className="register-card">
        <Typography className="register-title" component="h1">
          Register
        </Typography>

        <form className="register-form" onSubmit={handleSubmit}>
          <TextField
            label="Name"
            name="name"
            required
            fullWidth
            margin="normal"
            value={form.name}
            onChange={handleChange}
          />

          <TextField
            label="Email"
            name="email"
            type="email"
            required
            fullWidth
            margin="normal"
            value={form.email}
            onChange={handleChange}
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
          />

          <div className="register-divider" />

          <Button type="submit" variant="contained" fullWidth className="register-button">
            Register
          </Button>
        </form>
      </Paper>
    </Box>
  );
};
