import { FC, useState } from 'react';
import {
  Box,
  TextField,
  Button,
  Typography,
  Paper,
  IconButton,
  InputAdornment,
  Alert,
  Snackbar,
} from '@mui/material';
import Visibility from '@mui/icons-material/Visibility';
import VisibilityOff from '@mui/icons-material/VisibilityOff';

import './Register.css';
import { AuthApiClient } from '../../api/clients/AuthApiClient';
import { UserDto } from '../../api/models/UserDto';
import { ErrorResponse } from '../../api/models/ErrorResponse';

export const Register: FC = () => {
  const [form, setForm] = useState({
    name: '',
    email: '',
    cnp: '',
    password: '',
    confirmPassword: '',
  });

  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);

  const [errors, setErrors] = useState({
    name: '',
    email: '',
    cnp: '',
    password: '',
    confirmPassword: '',
  });

  const [serverError, setServerError] = useState('');
  const [success, setSuccess] = useState('');

  const validateEmail = (email: string) =>
    /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);

  const validate = () => {
    let valid = true;
    const newErrors = {
      name: '',
      email: '',
      cnp: '',
      password: '',
      confirmPassword: '',
    };

    if (!form.name.trim()) {
      newErrors.name = 'Name is required';
      valid = false;
    }

    if (!form.email || !validateEmail(form.email)) {
      newErrors.email = 'Invalid email format';
      valid = false;
    }

    if (!form.cnp) {
      newErrors.cnp = 'CNP is required';
      valid = false;
    } else if (!/^\d{13}$/.test(form.cnp)) {
      newErrors.cnp = 'CNP must contain exactly 13 digits';
      valid = false;
    }

    if (!form.password) {
      newErrors.password = 'Password is required';
      valid = false;
    } else if (form.password.length < 6) {
      newErrors.password = 'Password must be at least 6 characters';
      valid = false;
    }

    if (form.confirmPassword !== form.password) {
      newErrors.confirmPassword = 'Passwords do not match';
      valid = false;
    }

    setErrors(newErrors);
    return valid;
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;

    // 🔥 Restricție numerică strictă pentru CNP
    if (name === 'cnp') {
      if (/^\d*$/.test(value) && value.length <= 13) {
        setForm({ ...form, cnp: value });
      }
      return;
    }

    setForm({ ...form, [name]: value });
    setErrors({ ...errors, [name]: '' });
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (!validate()) return;

    setServerError('');
    setSuccess('');

    const userDto: UserDto = {
      name: form.name,
      mail: form.email,
      password: form.password,
      CNP: form.cnp,
    };

    AuthApiClient.authenticateUser(userDto)
      .then((id) => {
        setSuccess('Your account was created! A confirmation email was sent!');
        setForm({
          name: '',
          email: '',
          cnp: '',
          password: '',
          confirmPassword: '',
        });

        // Clear message after 3 seconds
        setTimeout(() => {
          setSuccess('');
        }, 3000);
      })
      .catch((err: ErrorResponse) => {
        console.log(err);
        setServerError(err.message!);

        // Clear message after 3 seconds
        setTimeout(() => setServerError(''), 3000);
      });

    console.log('Register data:', form);
  };

  return (
    <Box className="register-page">
      <Paper elevation={3} className="register-card">
        <Snackbar
          open={Boolean(serverError)}
          autoHideDuration={3000}
          onClose={() => setServerError('')}
          anchorOrigin={{ vertical: 'top', horizontal: 'right' }}
          className="toast-snackbar">
          <Alert
            severity="error"
            onClose={() => setServerError('')}
            className="toast-alert">
            {serverError}
          </Alert>
        </Snackbar>

        <Snackbar
          open={Boolean(success)}
          autoHideDuration={3000}
          onClose={() => setSuccess('')}
          anchorOrigin={{ vertical: 'top', horizontal: 'right' }}
          className="toast-snackbar">
          <Alert
            severity="success"
            onClose={() => setSuccess('')}
            className="toast-alert">
            {success}
          </Alert>
        </Snackbar>
        <Typography className="register-title" component="h1" variant="h4">
          Register
        </Typography>

        <form className="register-form" onSubmit={handleSubmit}>
          <TextField
            label="Name"
            name="name"
            fullWidth
            margin="normal"
            value={form.name}
            onChange={handleChange}
            error={Boolean(errors.name)}
            helperText={errors.name}
          />

          <TextField
            label="Email"
            name="email"
            type="email"
            fullWidth
            margin="normal"
            value={form.email}
            onChange={handleChange}
            error={Boolean(errors.email)}
            helperText={errors.email}
          />

          {/* 🔥 CNP FIELD */}
          <TextField
            label="CNP"
            name="cnp"
            fullWidth
            margin="normal"
            value={form.cnp}
            onChange={handleChange}
            error={Boolean(errors.cnp)}
            helperText={errors.cnp}
            inputProps={{ maxLength: 13 }}
          />

          {/* PASSWORD FIELD */}
          <TextField
            label="Password"
            name="password"
            type={showPassword ? 'text' : 'password'}
            fullWidth
            margin="normal"
            value={form.password}
            onChange={handleChange}
            error={Boolean(errors.password)}
            helperText={errors.password}
            InputProps={{
              endAdornment: (
                <InputAdornment position="end">
                  <IconButton onClick={() => setShowPassword(!showPassword)}>
                    {showPassword ? <VisibilityOff /> : <Visibility />}
                  </IconButton>
                </InputAdornment>
              ),
            }}
          />

          <TextField
            label="Confirm Password"
            name="confirmPassword"
            type={showConfirmPassword ? 'text' : 'password'}
            fullWidth
            margin="normal"
            value={form.confirmPassword}
            onChange={handleChange}
            error={Boolean(errors.confirmPassword)}
            helperText={errors.confirmPassword}
            InputProps={{
              endAdornment: (
                <InputAdornment position="end">
                  <IconButton
                    onClick={() =>
                      setShowConfirmPassword(!showConfirmPassword)
                    }>
                    {showConfirmPassword ? <VisibilityOff /> : <Visibility />}
                  </IconButton>
                </InputAdornment>
              ),
            }}
          />

          <Button
            type="submit"
            variant="contained"
            fullWidth
            className="register-button">
            🚗 Register
          </Button>
        </form>
      </Paper>
    </Box>
  );
};

export default Register;
